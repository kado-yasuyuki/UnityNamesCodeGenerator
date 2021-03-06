﻿using System.Collections.Generic;
using System.Linq;

namespace NamesCode.Generator.CodeBuilder
{
    public class StaticClassCodeBuilder
    {
        private readonly CodeStringBuilder _builder = new CodeStringBuilder();

        public StaticClassCodeBuilder AddHeaderCommend(string headerComment)
        {
            _builder.AddHeaderCommend(headerComment);
            return this;
        }

        public StaticClassCodeBuilder AddNamespace(string namespaceName)
        {
            _builder.AddNamespace(namespaceName);
            return this;
        }

        public StaticClassCodeBuilder AddClass(string className)
        {
            _builder.AppendIndentLine(string.Format("public static class {0}", className));
            _builder.IncreaseIndent();
            return this;
        }

        public StaticClassCodeBuilder AddObjectParameters(string typeName, IEnumerable<NameWithNumber> nameWithNumbers)
        {
            var nameWithNumbersArray = nameWithNumbers.ToArray();
            var parameters = nameWithNumbersArray
                .Select(n => GenerateStructProperty(typeName, CodeStringUtils.ConvertToVariableName(n.Name), CodeStringUtils.SurroundWithDoubleQuote(n.Name), n.Number.ToString()));
            foreach (var parameter in parameters)
                _builder.AppendIndentLine(parameter);

            if(nameWithNumbersArray.Length > 0)
                _builder.AddEmptyLine();

            _builder.AppendIndentLine(string.Format("public static readonly {0}[] Names =", typeName));
            _builder.IncreaseIndent();

            var variables = nameWithNumbersArray
                .Select(n => n.Name)
                .Select(CodeStringUtils.ConvertToVariableName);
            foreach (var v in variables)
            {
                _builder.AppendIndentLine(v + ",");
            }
            
            _builder.CloseIndent();
            return this;
        }

        public string Build()
        {
            return _builder.Build();
        }

        private static string GenerateStructProperty(string typeName, string variableName, params string[] constructorArguments)
        {
            var args = string.Join(", ", constructorArguments);
            return string.Format("public static readonly {0} {1} = new {0}({2});", typeName, variableName, args);
        }
    }
}
