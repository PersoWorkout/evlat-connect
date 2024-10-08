﻿using Application.Users;
using Domain.Users;

namespace Application.UnitTest.Fakers.Repositories
{
    public class FakeUserRepository : IUserRepostory
    {
        public List<User> Users { get; set; } = [];

        public async Task<User> Create(User user)
        {
            Users.Add(user);
            return user;
        }

        public async Task<int> Delete(User user)
        {
            var index = Users.FindIndex(x => x.Id == user.Id);

            if (index == -1) return 0;

            Users.RemoveAt(index);
            return 1;
        }

        public async Task<string> GetNewUsername(string firstname, string lastname)
        {
            var username = firstname.Trim() + "." + lastname.Trim();
            var counter = Users
                .Where(x => x.Username.StartsWith(username))
                .Count();

            return username + (counter > 0 ? counter : "");
        }

        public Task<IEnumerable<User>> GetProfessors()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetStudents()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserById(Guid id)
        {
            return Users.FirstOrDefault(x => x.Id == id);
        }

        public Task<IEnumerable<User>> GetUsers()
        {
            throw new NotImplementedException();
        }

        public async Task<User> Update(User user)
        {
            var idx = Users.FindIndex(x => x.Id == user.Id);

            if (idx == -1)
                return null;

            Users[idx] = user;
            return user;
        }

        public void ClearUsers()
        {
            Users = [];
        }
    }
}
