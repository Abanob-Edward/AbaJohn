﻿using AbaJohn.ViewModel;

namespace AbaJohn.Services.user
{
    public interface Iuser
    {
         UserWithaddressViewModel GetUserInfo (string username) ;
         int UpdateUserInfo (UserWithaddressViewModel user);
    }
}
