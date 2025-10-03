using VetClinic.Management.Api.Infrastructure;
using VetClinic.Management.Domain.Entities;

public class ManagementRepository(ManagementDbContext dbContext) 
    : IManagementRepository
{
    private readonly ManagementDbContext _dbContext = dbContext;

    public Pet? GetById(Guid id)
    {
        return _dbContext.Pets.Find(id);
    }

    public IEnumerable<Pet> GetAll()
    {
        return _dbContext.Pets.ToList();
    }
    public void Add(Pet pet)
    {
        _dbContext.Pets.Add(pet);
        _dbContext.SaveChanges();
    }
    
    public void Update(Pet pet)
    {
        _dbContext.Pets.Update(pet);
        _dbContext.SaveChanges();
    }
    public void Delete(Pet pet)
    {
        _dbContext.Pets.Remove(pet);
        _dbContext.SaveChanges();
    }

}
