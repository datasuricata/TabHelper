using TabHelper.Models.Base;
using TabHelper.Services;

namespace TabHelper.Models.Entities
{
    public class Historic : EntityBase
    {
        #region [ properties ]

        public int UserId { get; private set; }
        public int TabulationId { get; private set; }

        public string Ip { get; private set; }
        public string FormJson { get; private set; }

        #endregion

        #region [ ctor ]

        protected Historic()
        {

        }

        public Historic(int userId, int tabulationId, string ip, string formJson)
        {
            Validate(userId, tabulationId, ip, formJson);
            SetProperties(userId, tabulationId, ip, formJson);
        }

        #endregion

        #region [ methods ]

        public void Validate(int userId, int tabulationId, string ip, string formJson)
        {
            DomainValidation.When(userId == 0, "Usuário deve estar logado para prosseguir");
            DomainValidation.When(tabulationId == 0, "Identificador da tabulação é obrigatório");
            DomainValidation.When(string.IsNullOrEmpty(ip), "Não foi possível rastrear seu endereço virtual, contato o suporte");
            DomainValidation.When(string.IsNullOrEmpty(formJson), "Formulário deve ser preenchido, se o problema persistir contate o suporte");
        }
        public void SetProperties(int userId, int tabulationId, string ip, string formJson)
        {
            UserId = userId;
            TabulationId = tabulationId;
            Ip = ip;
            FormJson = formJson;
        }

        #endregion
    }
}
