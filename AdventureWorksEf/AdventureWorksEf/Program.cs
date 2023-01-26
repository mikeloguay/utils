using AdventureWorksEf.Context;
using AdventureWorksEf.DataModel;
using Microsoft.EntityFrameworkCore;

using (var context = new AdventureWorksContext())
{
    //IQueryable<IEnumerable<Person>> ppp = context.People
    //    .GroupBy(p => p.EmailPromotion)
    //    .Select(g => g.OrderByDescending(p => p.BusinessEntityId)
    //        .Take(1));


    List<Person> peopleByEmailPromotion = await context.People
        .GroupBy(p => p.EmailPromotion)
        .Select(g => g.OrderByDescending(p => p.BusinessEntityId).FirstOrDefault())
        .ToListAsync();

    //List<IEnumerable<Person>> peopleGroups = await context.People
    //    .GroupBy(p => p.EmailPromotion)
    //    .Select(g => g.OrderByDescending(p => p.BusinessEntityId)
    //        .Take(1))
    //    .ToListAsync();

    //PrintInts(peopleByEmailPromotion);
    PrintPeople(peopleByEmailPromotion);
    //PrintPeopleGroups(peopleGroups);
}

void PrintInts(List<int> peopleByEmailPromotion)
{
    peopleByEmailPromotion.ForEach(p =>
    {
        Console.WriteLine(p);
    });
}

void PrintPeople(List<Person> peopleByEmailPromotion)
{
    peopleByEmailPromotion.ForEach(p =>
    {
        Console.WriteLine($"{nameof(p.BusinessEntityId)}={p.BusinessEntityId}"
            + $", {nameof(p.EmailPromotion)}={p.EmailPromotion}"
            + $", {nameof(p.FirstName)}={p.FirstName}"
            );
    });
}

void PrintPeopleGroups(List<IEnumerable<Person>> peopleGroups)
{
    peopleGroups.ForEach(pg
        =>
    {
        Person p = pg.First();
        
        Console.WriteLine($"{nameof(p.BusinessEntityId)}={p.BusinessEntityId}"
            + $", {nameof(p.EmailPromotion)}={p.EmailPromotion}"
            + $", {nameof(p.FirstName)}={p.FirstName}"
            );
    });
}