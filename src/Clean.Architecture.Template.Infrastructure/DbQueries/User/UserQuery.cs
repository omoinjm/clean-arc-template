using Clean.Architecture.Template.Core.Entity;
using Clean.Architecture.Template.Core.Specs;

namespace Clean.Architecture.Template.Infrastructure.DbQueries.User
{
    public class UserQuery : QueryBase
    {
        #region Get User Queries

        public override string Query(GeneralSpecParams generalSpecParams)
        {
            SetWhereClause(generalSpecParams);

            if (generalSpecParams.Sort == null)
                ApplySortExpression(GetDefaultSortField());

            var query = $@"

                {MainSelect()}

                WHERE 1=1 {_whereClause}        

                ORDER BY {_sortExpression}
        
                LIMIT {generalSpecParams.PageSize} OFFSET {(generalSpecParams.PageIndex - 1) * generalSpecParams.PageSize}

            ";

            return query;
        }

        public static string GetUserByEmailQuery(string email)
        {
            var query = $@"

                {MainSelect()}

                WHERE u.email = {email}
            ";

            return query;
        }

        public static string GetUserByIdQuery(int id)
        {
            var query = $@"

                {MainSelect()}

                WHERE u.id = {id}
            ";

            return query;
        }

        private static string MainSelect()
        {
            return $@"

                SELECT

                    u.id AS Id,
                    u.name AS Name,
                    u.surname AS Surname,
                    u.email AS Email,
                    u.password AS Password,
                    u.salt AS Salt,
                    u.change_password AS ChangePassword,
                    u.created_by AS CreatedBy,
                    u.updated_by AS UpdatedBy,
                    u.deleted_by AS DeletedBy,
                    u.synced_at AS SyncedAt,
                    u.created_at AS CreatedAt,
                    u.updated_at AS UpdatedAt,
                    u.deleted_at AS DeletedAt,
                    u.phone_number AS PhoneNumber,
                    u.role AS Role,

                    u.is_active AS IsActive,
                    u.login_date AS LoginDate,
                
                    u.status_id AS UserStatusId,
                    us.status AS UserStatusName,
                    us.symbol AS UserStatusSymbol,
                    us.color AS UserStatusColor
            
                FROM au_users u
                LEFT JOIN ui_user_status us ON us.id = u.status_id                
            ";
        }

        protected override string GetDefaultSortField()
        {
            return "u.name";
        }

        protected override void SetWhereClause(GeneralSpecParams generalSpecParams)
        {
            // Call the base method to include default conditions
            base.SetWhereClause(generalSpecParams);

            // Append additional conditions specific to UserQuery
            if (!string.IsNullOrEmpty(generalSpecParams.Search))
            {
                _whereClause += @$"
                AND (u.email LIKE '%{generalSpecParams.Search}%'
                OR u.name LIKE '%{generalSpecParams.Search}%'
                OR u.surname LIKE '%{generalSpecParams.Search}%'
                OR (u.name || ' ' || u.surname) LIKE '%{generalSpecParams.Search}%')
            ";
            }
        }

        #endregion

        #region Create, Update Delete User Queries

        public static string CreateUserQuery(UserEntity user)
        {
            var query = $@"

                INSERT INTO au_users
                (
                    name,
                    surname,
                    email,
                    password,
                    salt,
                    created_by,
                    created_at,
                    phone_number,
                    role,
                    is_active,
                    status_id,
                    deleted
                )
                VALUES
                (
                    {user.Name},
                    {user.Surname},
                    {user.Email},
                    {user.Password},
                    {user.Salt},
                    {user.CreatedBy},
                    NOW(),
                    {user.PhoneNumber},
                    {user.Role},
                    {user.IsActive},
                    {user.UserStatusId},
                    false
                )
                RETURNING id
            ";

            return query;
        }

        public static string UpdateUserQuery(UserEntity user)
        {
            var query = $@"

                UPDATE au_users SET

                    name = {user.Name},
                    role = {user.Role},
                    updated_at = NOW(),
                    surname = @surname,
                    status_id = @statusId,
                    id_number = @idNumber,
                    is_active = @isActive,
                    updated_by = @updatedBy,
                    login_date = @loginDate,
                    phone_number = @phoneNumber

            ";

            return query;
        }

        public static string DeleteUserQuery(int id)
        {

        }

        #endregion
    }
}
