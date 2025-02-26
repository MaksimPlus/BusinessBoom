using BusinessBoom.Data;
using BusinessBoom.DTO;
using BusinessBoom.Exceptions;
using BusinessBoom.Implementations;
using BusinessBoom.Models;

namespace BusinessBoom.Services
{
    public class UserService
    {
        private readonly ApplicationContext _context;
        public UserService(ApplicationContext context)
        {
            _context = context;
        }
        public User Create(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new EmailInvalidException($"Введенный E-mail {email} некорректен");
            User user = new User();
            using (var uow = new UoW(_context))
            {
                user.Id = Guid.NewGuid();
                user.Email = email;
                user.Balance = 0;
                uow.Repository.Create(user);
                uow.SaveChanges();
            }
            return user;
        }
        public UserResponseDto GetBalance(Guid id)
        {
            using (var uow = new UoW(_context))
            {
                var dto = uow.Repository.GetById(id);
                if (dto != null)
                    return dto.ToDto();
            }
            throw new WalletNotFoundException($"Кошелек с идентифкатором {id} не найден.");
        }
        public UserResponseDto Deposit(Guid userId, UserRequestDto user)
        {
            if (user.Amount <= 0)
                throw new DepositZeroException("Сумма депозита должна быть больше 0");
            using (var uow = new UoW(_context))
            {
                var dto = uow.Repository.GetById(userId);
                if (dto == null)
                    throw new WalletNotFoundException($"Кошелек с идентифкатором {userId} не найден.");
                dto.Balance += user.Amount;
                uow.SaveChanges();
                return dto.ToDto();
            }
        }
        public UserResponseDto Withdraw(Guid userId, UserRequestDto user)
        {
            if (user.Amount <= 0)
                throw new DepositZeroException("Сумма депозита должна быть больше 0");
            using (var uow = new UoW(_context))
            {
                var dto = uow.Repository.GetById(userId);
                if (dto == null)
                    throw new WalletNotFoundException($"Кошелек с идентифкатором {userId} не найден.");
                if (dto.Balance < user.Amount)
                    throw new BalanceLessDepositException("Недостаточно средств на балансе");
                dto.Balance -= user.Amount;
                uow.SaveChanges();
                return dto.ToDto();
            }
        }
    }
}
