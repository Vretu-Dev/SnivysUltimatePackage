using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Interfaces;
using SnivysFreeCustomRolesSC.FreeCustomRoles;

namespace SnivysFreeCustomRolesSC
{
    public class MasterConfig : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        
        [Description("These custom roles are for the server owner to make some custom roles themselves, there will be no support for any custom roles that you make with these")]
        public List<FreeCustomRole1> FreeCustomRoles1 { get; set; }
        public List<FreeCustomRole2> FreeCustomRoles2 { get; set; }
        public List<FreeCustomRole3> FreeCustomRoles3 { get; set; }
        public List<FreeCustomRole4> FreeCustomRoles4 { get; set; }
        public List<FreeCustomRole5> FreeCustomRoles5 { get; set; }
        public List<FreeCustomRole6> FreeCustomRoles6 { get; set; }
        public List<FreeCustomRole7> FreeCustomRoles7 { get; set; }
        public List<FreeCustomRole8> FreeCustomRoles8 { get; set; }
        public List<FreeCustomRole9> FreeCustomRoles9 { get; set; }
        public List<FreeCustomRole10> FreeCustomRoles10 { get; set; }
        public List<FreeCustomRole11> FreeCustomRoles11 { get; set; }
        public List<FreeCustomRole12> FreeCustomRoles12 { get; set; }
        public List<FreeCustomRole13> FreeCustomRoles13 { get; set; }
        public List<FreeCustomRole14> FreeCustomRoles14 { get; set; }
        public List<FreeCustomRole15> FreeCustomRoles15 { get; set; }
        public List<FreeCustomRole16> FreeCustomRoles16 { get; set; }
        public List<FreeCustomRole17> FreeCustomRoles17 { get; set; }
        public List<FreeCustomRole18> FreeCustomRoles18 { get; set; }
        public List<FreeCustomRole19> FreeCustomRoles19 { get; set; }
        public List<FreeCustomRole20> FreeCustomRoles20 { get; set; }
        
        public void LoadConfigs()
        {
            FreeCustomRoles1 = new List<FreeCustomRole1>() { new FreeCustomRole1() };
            FreeCustomRoles2 = new List<FreeCustomRole2>() { new FreeCustomRole2() };
            FreeCustomRoles3 = new List<FreeCustomRole3>() { new FreeCustomRole3() };
            FreeCustomRoles4 = new List<FreeCustomRole4>() { new FreeCustomRole4() };
            FreeCustomRoles5 = new List<FreeCustomRole5>() { new FreeCustomRole5() };
            FreeCustomRoles6 = new List<FreeCustomRole6>() { new FreeCustomRole6() };
            FreeCustomRoles7 = new List<FreeCustomRole7>() { new FreeCustomRole7() };
            FreeCustomRoles8 = new List<FreeCustomRole8>() { new FreeCustomRole8() };
            FreeCustomRoles9 = new List<FreeCustomRole9>() { new FreeCustomRole9() };
            FreeCustomRoles10 = new List<FreeCustomRole10>() { new FreeCustomRole10() };
            FreeCustomRoles11 = new List<FreeCustomRole11>() { new FreeCustomRole11() };
            FreeCustomRoles12 = new List<FreeCustomRole12>() { new FreeCustomRole12() };
            FreeCustomRoles13 = new List<FreeCustomRole13>() { new FreeCustomRole13() };
            FreeCustomRoles14 = new List<FreeCustomRole14>() { new FreeCustomRole14() };
            FreeCustomRoles15 = new List<FreeCustomRole15>() { new FreeCustomRole15() };
            FreeCustomRoles16 = new List<FreeCustomRole16>() { new FreeCustomRole16() };
            FreeCustomRoles17 = new List<FreeCustomRole17>() { new FreeCustomRole17() };
            FreeCustomRoles18 = new List<FreeCustomRole18>() { new FreeCustomRole18() };
            FreeCustomRoles19 = new List<FreeCustomRole19>() { new FreeCustomRole19() };
            FreeCustomRoles20 = new List<FreeCustomRole20>() { new FreeCustomRole20() };
        }
    }
}