using PharmacyIMS.Enums;
using PharmacyIMS.Models;

namespace PharmacyIMS.Helpers
{
    /// <summary>
    /// 全局会话类，保存当前登录用户信息
    /// </summary>
    public static class Session
    {
        public static User? CurrentUser { get; private set; }

        public static bool IsLoggedIn => CurrentUser != null;

        public static string UserName => CurrentUser?.RealName ?? "未登录";

        public static UserRole? Role => CurrentUser?.Role;

        public static void Login(User user)
        {
            CurrentUser = user;
        }

        public static void Logout()
        {
            CurrentUser = null;
        }
    }
}
