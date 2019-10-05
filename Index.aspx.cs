using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rakuten.Test.Web.User
{
    public partial class Index : System.Web.UI.Page
    {

        private  UserService.UserSoap _userService;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Index()
        {
            _userService = new UserService.UserSoapClient();
            log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~//log4net.config")));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                UserService.GetUsersResponse _response = _userService.GetUsers(new UserService.GetUsersRequest { Body = new UserService.GetUsersRequestBody() });
                UserService.User[] _users = _response.Body.GetUsersResult.Data;

                this.ListUsers.DataSource = _users;
                this.ListUsers.DataBind();
            }
            catch (Exception ex)
            {
                this.MessageStatus.Text = "<div class='alert alert-danger alert-dismissible fade in' role='alert'> <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button> <strong>Listar Usuários</strong><br /> Ocorreu o seguinte problema na operação: " + ex.Message + "</div>";
                log.Error("Erro: " + ex.Message);
            }            
        }
    }
}