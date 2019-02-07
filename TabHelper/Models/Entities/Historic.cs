using TabHelper.Models.Base;
using TabHelper.Services;

namespace TabHelper.Models.Entities
{
    public class Historic : EntityBase
    {
        #region [ properties ]

        public int TabulationId { get; private set; }
        public Tabulation Tabulation { get; private set; }

        public int UserId { get; private set; }
        public string Ip { get; private set; }
        public string FormJson { get; private set; }

        #endregion

        #region [ ctor ]

        protected Historic()
        {

        }

        public Historic(int tabulationId, int userId, string ip, string formJson)
        {
            Validate(tabulationId, userId, ip, formJson);
            SetProperties(tabulationId, userId, ip, formJson);
        }

        #endregion

        #region [ methods ]

        private void Validate(int tabulationId, int userId, string ip, string formJson)
        {
            DomainValidation.When(tabulationId == 0, "Identificador da tabulação é obrigatório");
            DomainValidation.When(userId == 0, "Usuario deve estar logado para continuar");
            DomainValidation.When(string.IsNullOrEmpty(ip), "Endereço ip não identificado, contate o suporte");
            DomainValidation.When(string.IsNullOrEmpty(formJson), "Não foi possível salvar o registro do formulário, contate o suporte");
        }

        private void SetProperties(int tabulationId, int userId, string ip, string formJson)
        {
            TabulationId = tabulationId;
            UserId = userId;
            Ip = ip;
            FormJson = formJson;
        }

        #endregion
    }
}
