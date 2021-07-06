namespace ExpressionTreeVisualizer.UIDemo {
    public record TestObject(string Category, string Source, string Name, object O) {
        public TestObject((string category, string source, string name, object o) x) : 
            this(x.category, x.source, x.name, x.o) { }
    }
}
