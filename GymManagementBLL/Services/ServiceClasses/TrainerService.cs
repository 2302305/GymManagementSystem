


namespace GymManagementBLL.Services.ServiceClasses
{
    public class TrainerService(IUnitOfWork unitOfWork) : ITrainerServices
    {
        public bool CreateTrainer(CreateTrainerViewModel createTrainerViewModel)
        {
            try
            {
                if (createTrainerViewModel == null) return false;
                var Repo = unitOfWork.GetRepository<Trainer>();
                var Email = createTrainerViewModel.Email;
                var Phone = createTrainerViewModel.Phone;
                var EmailAndPhoneCheck = Repo.GetAll().Any(s => s.Email == Email || s.Phone == Phone);
                if (EmailAndPhoneCheck) return false;

                var NewTrainer = new Trainer()
                {
                    Email = createTrainerViewModel.Email,
                    Phone = createTrainerViewModel.Phone,
                    Name = createTrainerViewModel.Name,
                    DateOfBirth = createTrainerViewModel.DateOfBirth,
                    Gender = createTrainerViewModel.Gender,
                    Specialities = createTrainerViewModel.Specialities,
                    Address = new CompositeAddressProperty()
                    {
                        BuildingNumber = createTrainerViewModel.BuildingNumber,
                        City = createTrainerViewModel.City,
                        Street = createTrainerViewModel.Street
                    },

                };
                Repo.Add(NewTrainer);
                return unitOfWork.SaveChanges() > 0;


            }
            catch (Exception)
            {
                return false;
            }


        }
        public IEnumerable<TrainersViewModel> GetAllTrainers()
        {
            var Trainers = unitOfWork.GetRepository<Trainer>().GetAll();
            if (Trainers == null) return [];
            return Trainers.Select(t => new TrainersViewModel()
            {
                Email = t.Email,
                Name = t.Name,
                Phone = t.Phone,
                Specialities = t.Specialities,
                Id = t.Id

            });

        }
        public TrainerDetailsViewModel? GetTrainerById(int Trainerid)
        {
            var Trainer = unitOfWork.GetRepository<Trainer>().GetById(Trainerid);
            if (Trainer == null) return null;
            return new TrainerDetailsViewModel()
            {
                BuildingNumber = Trainer.Address.BuildingNumber,
                DateOfBirth = Trainer.DateOfBirth,
                Name = Trainer.Name,
                Phone = Trainer.Phone,
                City = Trainer.Address.City,
                Street = Trainer.Address.Street,
                Specialization = Trainer.Specialities,
                Email = Trainer.Email
            };
        }

        public TrainerUpdateViewModel? UpdateTrainerViewModel(int Trainerid)
        {
            var Trainer = unitOfWork.GetRepository<Trainer>().GetById(Trainerid);
            if (Trainer == null) return null;
            return new TrainerUpdateViewModel()
            {
                Email = Trainer.Email,
                Name = Trainer.Name,
                City = Trainer.Address.City,
                Specialization = Trainer.Specialities,
                BuildingNumber = Trainer.Address.BuildingNumber,
                Phone = Trainer.Phone,
                Street = Trainer.Address.Street
            };
        }

        public bool UpdateTrainer(int Trainerid, TrainerUpdateViewModel trainerUpdateViewModel)
        {
            try
            {
                var Trainer = unitOfWork.GetRepository<Trainer>().GetById(Trainerid);
                if (trainerUpdateViewModel == null || Trainer == null) return false;
                var Repo = unitOfWork.GetRepository<Trainer>();
                var Email = trainerUpdateViewModel.Email;
                var EmailAndPhoneCheck = Repo.GetAll().Any(s => s.Email == trainerUpdateViewModel.Email && s.Id != Trainerid);
                if (EmailAndPhoneCheck) return false;
                Trainer.Name = trainerUpdateViewModel.Name;
                Trainer.Email = trainerUpdateViewModel.Email;
                Trainer.Specialities = trainerUpdateViewModel.Specialization;
                Trainer.Address.City = trainerUpdateViewModel.City;
                Trainer.Address.Street = trainerUpdateViewModel.Street;
                Trainer.Address.BuildingNumber = trainerUpdateViewModel.BuildingNumber;
                Trainer.UpdatedAt = DateTime.Now;

                unitOfWork.GetRepository<Trainer>().Update(Trainer);
                return unitOfWork.SaveChanges() > 0;

            }
            catch (Exception)
            {
                return false;
            }



        }
        public bool DeleteTrainer(int Trainerid)
        {
            try
            {
                var Repo = unitOfWork.GetRepository<Trainer>();
                var Trainer = Repo.GetById(Trainerid);
                if (Trainer == null) return false;
                var hasActiveSessions = unitOfWork.GetRepository<Session>()
               .GetAll(s => s.TrainerId == Trainerid && s.StartDate > DateTime.Now).Any();
                if (hasActiveSessions) return false;

                Repo.Delete(Trainer);
                return unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }


        }



    }
}
