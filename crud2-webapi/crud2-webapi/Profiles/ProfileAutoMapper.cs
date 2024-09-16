using AutoMapper;
using crud2_webapi.Dto;
using crud2_webapi.Models;

namespace crud2_webapi.Profiles
{
    public class ProfileAutoMapper : Profile
    {
        public ProfileAutoMapper()
        {
            //transformando o aluno em alunolistardto
            CreateMap<Aluno, AlunoListarDto>();
        }
    }
}
