using Application.ViewModels;
using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class RentACarProfile : Profile
    {
        public RentACarProfile()
        {
            CreateMap<Car, CarViewModel>().ReverseMap();
            CreateMap<Client, ClientViewModel>().ReverseMap();
        }
    }
}
