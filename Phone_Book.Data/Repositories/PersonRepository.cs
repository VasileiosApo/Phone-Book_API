using Microsoft.EntityFrameworkCore;
using Phone_Book.Data.Models;

namespace Phone_Book.Data.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly PersonContext _ctx;
    public PersonRepository(PersonContext ctx)
    {
        _ctx = ctx;
    }
    public async Task<IEnumerable<Person>> GetPeopleAsync()
    {
        var people = await _ctx.People.ToListAsync();
        return people;
    }
    public async Task<Person> GetPeopleByIdAsync(int id)
    {
        return await _ctx.People.FindAsync(id);
    }
    public async Task<Person> CreatePersonAsync(Person person)
    {
        _ctx.People.Add(person);
        await _ctx.SaveChangesAsync();
        return person;
    }
    public async Task UpdatePersonAsync(Person person)
    {
        _ctx.People.Update(person);
        await _ctx.SaveChangesAsync();
    }
    public async Task DeletePersonAsync(Person person)
    {
        _ctx.People.Remove(person);
        await _ctx.SaveChangesAsync();
    }
}
