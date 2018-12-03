namespace Csv.Convert
{
    public struct FieldValue
    {
        public string Name { get; }
        public string Value { get; }

        public FieldValue(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public override bool Equals(object obj) =>
            obj is FieldValue fv
            && fv.Name == Name
            && fv.Value == Value;
    }
}