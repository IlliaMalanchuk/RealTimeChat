namespace WebApi.Services
{
    public class ChatService
    {
        private readonly Dictionary<string, string> Users = new Dictionary<string, string>();

        public bool AddUserToList(string addUser)
        {
            lock (Users) // Add lock to avoid two users connected at the same time
            {
                foreach (var user in Users)
                {
                    if (user.Key.ToLower() == addUser.ToLower()) // username should be unique to add to the list so here we check it 
                    {
                        return false;
                    }
                }

                Users.Add(addUser, null);
                return true;
            }
        }

        public void AddUsersConnectionId(string user, string connectionId)
        {
            lock (Users)
            {
                if (Users.ContainsKey(user))
                {
                    Users[user] = connectionId;
                }
            }
        }

        public string GetUserByConnectionId(string connectionId)
        {
            lock (Users)
            {
                return Users.Where(x => x.Value == connectionId).Select(x => x.Key).FirstOrDefault();
            }
        }

        public string GetConnectionIdByUser(string user)
        {
            lock (Users)
            {
                return Users.Where(x => x.Value == user).Select(x => x.Key).FirstOrDefault();
            }
        }

        public void RemoveUserFromList(string user)
        {
            lock (Users)
            {
                if (Users.ContainsKey(user))
                {
                    Users.Remove(user);
                }
            }
        }

        public string[] GetOnlineUsers()
        {
            lock (Users)
            {
                return Users.OrderBy(x => x.Key).Select(x => x.Key).ToArray();
            }
        }
    }
}
