using AdventureWorksEf.Context;
using AdventureWorksEf.DataModel;
using Microsoft.EntityFrameworkCore;

using (var context = new AdventureWorksContext())
{
    //var addressTypes = await context.AddressTypes.ToListAsync();
    //addressTypes.ForEach(a => Console.WriteLine(a.Name));

    var person = await context.People.SingleAsync(p => p.BusinessEntityId == 20777);

    var emailAddress = new EmailAddress
    {
        EmailAddress1 = "mikelus@home.test"
    };

    person.EmailAddresses.Add(emailAddress);
    int rowsAffected = context.SaveChanges();

    Console.WriteLine($"rowsAffected={rowsAffected}. EmailAddressID={emailAddress.EmailAddressId}");
}