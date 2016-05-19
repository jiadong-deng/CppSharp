﻿using System;
using CppSharp.AST;
using CppSharp.Utils;

namespace CppSharp.Generator.Tests
{
    public class ASTTestFixture
    {
        protected Driver Driver;
        protected DriverOptions Options;
        protected ASTContext AstContext;

        protected void ParseLibrary(params string[] files)
        {
            Options = new DriverOptions();

            var testsPath = GeneratorTest.GetTestsDirectory("Native");
            Options.Module.IncludeDirs.Add(testsPath);

            Options.Headers.AddRange(files);

            Driver = new Driver(Options, new TextDiagnosticPrinter());
            foreach (var includeDir in Options.Module.IncludeDirs)
                Options.addIncludeDirs(includeDir);
            Driver.SetupIncludes();
            Driver.BuildParseOptions();
            if (!Driver.ParseCode())
                throw new Exception("Error parsing the code");

            AstContext = Driver.ASTContext;
        }
    }
}
