using VetClinic.Management.Domain.Entities;

public interface IManagementRepository
{
    Pet? GetById (Guid id);
    IEnumerable<Pet> GetAll();
    void Add(Pet pet);
    void Update(Pet pet);
    void Delete(Pet pet);
}
