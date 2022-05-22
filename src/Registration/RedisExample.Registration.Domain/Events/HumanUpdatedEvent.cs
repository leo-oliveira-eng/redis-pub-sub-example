using RedisExample.Registration.Domain.Models;

namespace RedisExample.Registration.Domain.Events
{
    public class HumanUpdatedEvent : HumanEvent
    {
        public List<PetEvent> Pets { get; set; } = new List<PetEvent>();

        public HumanUpdatedEvent(Human human)
            : base(human!.Code, human!.CPF.Text, human!.Name, human!.Email.Address, human!.BirthDate, human!.Gender, human!.Address, human!.PhoneNumber)
        {
            Pets = human.Pets.Select(pet => 
                new PetEvent(pet.Code
                    , pet.Name
                    , pet.BirthDate
                    , pet.Species
                    , pet.Color
                    , pet.Breed
                    , pet.Vaccines.Select(vaccine => new VaccineEvent(vaccine, pet.Code)).ToList()
                    , human.Code)).ToList();
        }
    }
}
