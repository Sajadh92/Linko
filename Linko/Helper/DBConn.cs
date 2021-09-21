
namespace Linko.Helper
{
    public enum ConnectionType
    {
        SqlServerLocal,
        SqlServerSmarter
    }

    public static class DBConn
    {
        public static readonly ConnectionType ConnectionType = ConnectionType.SqlServerLocal;

        public static string ConnectionString
        {
            get
            {
                return ConnectionType switch
                {
                    ConnectionType.SqlServerLocal => "Server=.\\SAJJADH92; Database=LinkoDB;" +
                        "User Id=sa; Password=Sajode; MultipleActiveResultSets=True;",

                    ConnectionType.SqlServerSmarter => "",

                    _ => "",
                };
            }
        }
    }
}