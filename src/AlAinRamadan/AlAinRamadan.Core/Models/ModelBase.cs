namespace AlAinRamadan.Core.Models
{
    public class ModelBase
    {
        public int Id { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is null || GetType() != obj.GetType()) return false;
            return Id == (obj as ModelBase).Id;
        }
        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator ==(ModelBase left, ModelBase right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (left is null || right is null) return false;
            return left.Equals(right);
        }

        public static bool operator !=(ModelBase left, ModelBase right) => !(left == right);
    }
}
