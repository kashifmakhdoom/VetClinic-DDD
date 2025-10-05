using VetClinic.Management.Domain.Entities;
using VetClinic.Management.Domain.ValueObjects;
using VetClinic.SharedKernel.ValueObjects;

namespace VetClinic.Management.Domain.Tests
{
    public class Pet_Tests
    {
        [Fact]
        public void Pet_should_be_equal()
        {
            // Arrange
            var id = Guid.NewGuid();
            var breedService = new FakeBreedService();
            var breedId = new BreedId(breedService.breeds[0].Id, breedService);

            var pet1 = new Pet(id, "Brutus", 15, "Gray", GenderOfPet.Male, breedId);
            var pet2 = new Pet(id, "Nina", 10, "White", GenderOfPet.Female, breedId);

            // Assert
            Assert.True(pet1.Equals(pet2));
        }

        [Fact]
        public void Pet_should_be_equal_using_operators()
        {
            // Arrange
            var id = Guid.NewGuid();
            var breedService = new FakeBreedService();
            var breedId = new BreedId(breedService.breeds[0].Id, breedService);

            var pet1 = new Pet(id, "Brutus", 15, "Gray", GenderOfPet.Male, breedId);
            var pet2 = new Pet(id, "Nina", 10, "White", GenderOfPet.Female, breedId);

            // Assert
            Assert.True(pet1 == pet2);
        }

        [Fact]
        public void Pet_should_not_be_equal_using_operators()
        {
            // Arrange
            var breedService = new FakeBreedService();
            var breedId = new BreedId(breedService.breeds[0].Id, breedService);

            var pet1 = new Pet(Guid.NewGuid(), "Brutus", 15, "Gray", GenderOfPet.Male, breedId);
            var pet2 = new Pet(Guid.NewGuid(), "Nina", 10, "White", GenderOfPet.Female, breedId);

            // Assert
            Assert.True(pet1 != pet2);
        }

        [Fact]
        public void Weight_should_be_equal()
        {
            // Arrange
            var weight1 = new Weight(12.3);
            var weight2 = new Weight(12.3);

            // Assert
            Assert.True(weight1 == weight2);
        }

        [Fact]
        public void Weight_should_not_be_equal()
        {
            // Arrange
            var weight1 = new Weight(12.3);
            var weight2 = new Weight(15.3);

            // Assert
            Assert.True(weight1 != weight2);
        }

        [Fact]
        public void WeightRange_should_be_equal()
        {
            // Arrange
            var weightRange1 = new WeightRange(8.3, 15.0);
            var weightRange2 = new WeightRange(8.3, 15.0);

            // Assert
            Assert.True(weightRange1 == weightRange2);
        }

        [Fact]
        public void WeightRange_should_not_be_equal()
        {
            // Arrange
            var weightRange1 = new WeightRange(7.0, 17.0);
            var weightRange2 = new WeightRange(8.0, 15.0);

            // Assert
            Assert.True(weightRange1 != weightRange2);
        }

        [Fact]
        public void BreedId_should_be_valid()
        {
            // Arrange
            var breedService = new FakeBreedService();
            var id = breedService.breeds[0].Id;
            var breedId = new BreedId(id, breedService);

            // Assert
            Assert.NotNull(breedId);
        }

        [Fact]
        public void BreedId_should_not_be_valid()
        {
            // Arrange
            var breedService = new FakeBreedService();
            var id = Guid.NewGuid();

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                var breedId = new BreedId(id, breedService);
            });
        }

        [Fact]
        public void WeightClass_should_be_ideal()
        {
            // Arrange
            var breedService = new FakeBreedService();
            var breedId = new BreedId(breedService.breeds[0].Id, breedService);
            var pet = new Pet(Guid.NewGuid(), "Brutus", 15, "Gray", GenderOfPet.Male, breedId);
            pet.SetWeight(14, breedService);

            // Assert
            Assert.True(pet.WeightClass == WeightClass.Ideal);
        }

        [Fact]
        public void WeightClass_should_be_underweight()
        {
            // Arrange
            var breedService = new FakeBreedService();
            var breedId = new BreedId(breedService.breeds[0].Id, breedService);
            var pet = new Pet(Guid.NewGuid(), "Brutus", 15, "Gray", GenderOfPet.Male, breedId);
            pet.SetWeight(8, breedService);

            // Assert
            Assert.True(pet.WeightClass == WeightClass.Underweight);
        }

        [Fact]
        public void WeightClass_should_be_overweight()
        {
            // Arrange
            var breedService = new FakeBreedService();
            var breedId = new BreedId(breedService.breeds[0].Id, breedService);
            var pet = new Pet(Guid.NewGuid(), "Brutus", 15, "Gray", GenderOfPet.Male, breedId);
            pet.SetWeight(30, breedService);

            // Assert
            Assert.True(pet.WeightClass == WeightClass.Overweight);
        }
    }
}
