using System;
using System.Collections.Generic;
using System.Text;

namespace Polygen.Core.Stage
{
    /// <summary>
    /// All stages in the code generation process.
    /// </summary>
    public enum StageType
    {
        #region Initialization stage
        Initialize,
        #endregion

        #region Schema registration stage
        RegisterSchemas,
        #endregion

        #region Project configuration parsing stage
        BeforeParseProjectConfiguration,
        AfterParseProjectConfiguration,
        ValidateProjectConfiguration,
        #endregion

        #region Template registration stage
        RegisterTemplates,
        #endregion

        #region Ouput configuration registration stage (e.g. for target platforms and output folders)
        ConfigureTargetPlatforms,
        InitializeOutputConfiguration,
        #endregion

        #region Input XML file parsing stage
        BeforeParseInputXmlFiles,
        ParseInputXmlFiles,
        AfterParseInputXmlFiles,
        #endregion

        #region Design model parsing stage
        BeforeParseDesignModels,
        ParseDesignModels,
        ResolveDesignModelReferences,
        AfterParseDesignModels,
        ValidateDesignModels,
        ApplyProjectLayout,
        #endregion

        #region Output model generation stage
        BeforeGenerateOutputModels,
        GenerateOutputModels,
        AfterOutputModelsGenerated,
        #endregion

        #region Output file writing stage
        BeforeWriteOutputFiles,
        AfterWriteOutputFiles,
        #endregion

        #region Output file copying stage
        BeforeCopyOutputFiles,
        AfterCopyOutputFiles,
        #endregion

        #region Cleanup stage
        Cleanup,
        Finished,
        #endregion
    }
}
