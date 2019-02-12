using System.Reflection;

namespace TabHelper.Helpers
{
    public static class Messenger
    {
        #region [ methods ]

        public static string Changed<T>(T obj)
        {
            var value = string.Empty;

            foreach (var p in obj.GetType().GetProperties())
                value = GetPropModel(obj, value, p);

            return $"{value} foi alterado com sucesso.";
        }

        public static string Created<T>(T obj)
        {
            var value = string.Empty;

            foreach (var p in obj.GetType().GetProperties())
                value = GetPropModel(obj, value, p);

            return $"{value} foi criado com sucesso.";
        }

        public static string Exclude<T>(T obj)
        {
            var value = string.Empty;

            foreach (var p in obj.GetType().GetProperties())
                value = GetPropModel(obj, value, p);

            return $"{value} foi excluido com sucesso.";
        }

        public static string SoftExclude<T>(T obj)
        {
            var value = string.Empty;

            foreach (var p in obj.GetType().GetProperties())
                value = GetPropModel(obj, value, p);

            return $"{value} foi desativado com sucesso.";
        }

        #endregion

        #region [ private ]

        private static string GetPropModel<T>(T obj, string value, PropertyInfo p)
        {
            if (p.Name == "Name")
                value = p.GetValue(obj).ToString();
            else if (p.Name == "Title")
                value = p.GetValue(obj).ToString();
            return value;
        }

        #endregion
    }
}
