using AbaJohn.Models;
using AbaJohn.ViewModel;

namespace AbaJohn.Services.user
{
    public class UserRepository : Iuser
    {
        private readonly ApplicationDbContext context;

        public UserRepository(ApplicationDbContext _context)
        {
            context = _context;
        }
        public string GenerateUniqueImageName()
        {
            // Get the current date and time
            DateTime now = DateTime.Now;

            // Generate a unique name using a combination of timestamp and random number
            string uniqueName = $"{now:yyyyMMddHHmmss}_{Guid.NewGuid().ToString("N").Substring(0, 8)}";

            // Return the unique name
            return uniqueName;
        }

        public UserWithaddressViewModel GetUserInfo(string username)
        {

            UserWithaddressViewModel userInfo = new UserWithaddressViewModel();
                var user = context.Users.FirstOrDefault(x => x.UserName == username);
                var useraddress = context.Addresses.FirstOrDefault(x => x.User_id == user.Id);

            if (user != null && useraddress != null) {
            
                userInfo.user_name = user.UserName;
                userInfo.name = user.Name;
                userInfo.phone_number = user.PhoneNumber;
                userInfo.email = user.Email;
                userInfo.Gender = user.Gender;
                userInfo.image = user.img;
                userInfo.age = user.age;
                //bind user adrress data
                userInfo.country = useraddress.country;
                  userInfo.city=         useraddress.city;
                 userInfo.home_number = useraddress.home_number;
                userInfo.street_name = useraddress.street_name;


                return userInfo;
            }

                return null;

        }

        public int UpdateUserInfo( UserWithaddressViewModel UserINFO)
        {
            var user = context.Users.FirstOrDefault(x => x.UserName == UserINFO.user_name);
            var useraddress = context.Addresses.FirstOrDefault(x => x.User_id == user.Id);


            string ProfileImage = "";
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/BussAccImg/SellerImg");

         

            if (UserINFO.ImageFile != null)
            {
                // Generate a unique file name for BaseImg
                ProfileImage = GenerateUniqueImageName() + Path.GetExtension(UserINFO.ImageFile.FileName);
                string ImageFileeImgFilePath = Path.Combine(path, ProfileImage);

                using (var stream = new FileStream(ImageFileeImgFilePath, FileMode.Create))
                {
                    UserINFO.ImageFile.CopyTo(stream);
                }

                // delete image after insert new image 
                string deletepath = path + "/" + UserINFO.image;
                FileInfo file = new FileInfo(deletepath);
                if (file.Exists)
                {
                    file.Delete();
                }

            }
            if (user != null && useraddress != null)
            {

             //bind user data
                user.Name = UserINFO.name;
                user.PhoneNumber = UserINFO.phone_number;
                user.Email = UserINFO.email;
                user.Gender = UserINFO.Gender;
                user.address = useraddress;
                user.img = ProfileImage;
                user.age = UserINFO.age;
                //bind user adrress data
                useraddress.country = UserINFO.country;
                useraddress.city = UserINFO.city;
                useraddress.home_number = UserINFO.home_number;
                useraddress.street_name = UserINFO.street_name; 
                
             
                context.SaveChanges();

                return 1;
            }

            return  0;
        }
    }
}
