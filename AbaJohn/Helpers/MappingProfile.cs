﻿using AbaJohn.Models;
using AbaJohn.ViewModel;
using AutoMapper;

namespace AbaJohn.Helpers
{
    public class MappingProfile: Profile 
    {
        public MappingProfile() {
            CreateMap<Product, productViewModel>()
                .ForMember(dest => dest.category_id,
                src => src.MapFrom(src => src.category.Id))
                .ForMember(dest=>dest.BaseImg , 
                src=>src.MapFrom(src=>src.images.FirstOrDefault().BaseImg))
                .ForMember(dest => dest.Img1,
                src => src.MapFrom(src => src.images.FirstOrDefault().Img1))
                .ForMember(dest => dest.Img2,
                src => src.MapFrom(src => src.images.FirstOrDefault().Img2))
                .ForMember(dest => dest.Img3,
                src => src.MapFrom(src => src.images.FirstOrDefault().Img3))
                
                ;
        } 
    }
}