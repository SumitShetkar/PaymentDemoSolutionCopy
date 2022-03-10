using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Model
{
    public class PrepDB
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SendData(serviceScope.ServiceProvider.GetService<PaymentDetailContext>());
            }
        }

        private static void SendData(PaymentDetailContext context)
        {
            Console.WriteLine("applying migration...");
            context.Database.Migrate();
            if(context.PaymentDetails.Any())
            {
                context.PaymentDetails.AddRange(new PaymentDetail()
                {
                    CardNumber = "12345678910111213141516",
                    CardOwnerName = "Pushpa Pushparaj",
                    ExpirationDate = "02/24",
                    SecurityCode = "123"
                });

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("already have data -- not seeding");
            }
        }
    }
}
