using System.Reflection;

namespace ProjetoTransicao.Shared.Enums
{
    public abstract class Enumeration : IComparable
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Text { get; private set; }
        public string Description { get; private set; }

        protected Enumeration(int id, string name, string text = null, string description = null) => (Id, Name, Text, Description) = (id, name, text, description);

        public override string ToString() => Name;

        public static IEnumerable<T> GetAll<T>() where T : Enumeration =>
            typeof(T).GetFields(BindingFlags.Public |
                                BindingFlags.Static |
                                BindingFlags.DeclaredOnly)
                     .Select(f => f.GetValue(null))
                     .Cast<T>();

        public override bool Equals(object obj)
        {
            if (obj is not Enumeration otherValue)
            {
                return false;
            }

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public int CompareTo(object obj) => Id.CompareTo(((Enumeration)obj).Id);
    }
}