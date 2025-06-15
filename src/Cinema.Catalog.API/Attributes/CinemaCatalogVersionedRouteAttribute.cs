using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace Cinema.Catalog.API.Attributes;

[ExcludeFromCodeCoverage]
public class CinemaCatalogVersionedRouteAttribute(string version) : RouteAttribute($"api/v{version}/[controller]") { }
