using AmpSampleService.APIs.Common;
using AmpSampleService.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace AmpSampleService.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class AiFindManyArgs : FindManyInput<Ai, AiWhereInput> { }
