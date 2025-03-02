using AutoMapper;
using Microsoft.AspNetCore.Http;
using PediVax.Repositories.IRepository;
using PediVax.Services.IService;

namespace PediVax.Services.Service;

public class ChildProfileService : IChildProfileService
{
    private readonly IChildProfileRepository _childProfileRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ChildProfileService(IChildProfileRepository childProfileRepository, IMapper mapper,
        IHttpContextAccessor httpContextAccessor)
    {
        _childProfileRepository = childProfileRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }
    
}