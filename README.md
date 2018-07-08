# ilys-ssr-client-csharp
配合sspanelv3mod的一个SSR电脑端的客户端（C#）

在 .\ilys-ssr-client-csharp\shadowsocks-csharpProgram.cs 里面

namespace GlobalVaribles
{
    // 全局变量申明位置
    public class GlobalVaribles
    {
        // 数据库配置模块
        public static string DB_Adress = "127.0.0.1";
        public static string DB_DBName = "sspanel";
        public static string DB_Username = "sspanel";
        public static string DB_Password = "password";
        // 登陆模块
        public static bool LOGIN_ifLogged = false;
        public static string LOGIN_username = "";
        public static string LOGIN_email = "";
    }
}

配置好sspanel_v3_mod_UIChanges里的数据库信息就能用了。

TIP:
等级过期时间为1987年（默认不过期的）会在软件内提示账户已过期。（一般是管理员账户）
