using Clean.Architecture.Template.Core.Specs;

namespace Clean.Architecture.Template.Infrastructure.DbQueries.Auth
{
    public class LoginTokenQuery : QueryBase
    {
        public override string Query(GeneralSpecParams generalSpecParams)
        {
            return $@"";
        }

        public static string Query(string email)
        {
            var query = $@"
                
                SELECT auth_token as AuthToken FROM au_user WHERE email = {email}
            
            ";

            return query;
        }

        protected override string GetDefaultSortField()
        {
            return "created_at";
        }
    }

}