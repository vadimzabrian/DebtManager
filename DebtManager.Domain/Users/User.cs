using DebtManager.Domain.Dtos;
using Vadim.Common;

namespace DebtManager.Domain.Entities
{
    public class User : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public UserDto ToDto()
        {
            UserDto userDto = new UserDto();

            userDto.Id = this.Id;
            userDto.Name = this.Name;

            return userDto;
        }
    }
}
