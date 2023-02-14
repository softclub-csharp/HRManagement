using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.SeedData;

public static  class SeedData
{
    public static void Seed(DataContext context)
    {

        var date = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);


    }
}