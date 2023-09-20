using login_api.data.Entities;

namespace login_api.data;

public interface IDbManager {

    User GetUser(User user);
    
}