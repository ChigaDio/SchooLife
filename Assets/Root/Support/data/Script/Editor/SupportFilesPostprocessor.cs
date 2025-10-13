#if UNITY_EDITOR
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using GameCore;

public class SupportFilesPostprocessor : IPostprocessBuildWithReport
{
    public int callbackOrder => 100;

    public void OnPostprocessBuild(BuildReport report)
    {
        string buildDir = Path.GetDirectoryName(report.summary.outputPath);
        if (string.IsNullOrEmpty(buildDir)) return;

        // �R�s�[�Ώۂ̃t�@�C���ƁA���ꂼ��� SupportChigadio/data �ȉ��̑��΃t�H���_
        var allFiles = new List<(string filePath, string targetSubFolder)>
        {
            (SupportFiles.ALL_SOUND_BIN, Path.Combine(SupportFiles.ASSETS_FOLDER, SupportFiles.SOUND_FOLDER)),
            (SupportFiles.ALL_GAMEOBJECT_BIN, Path.Combine(SupportFiles.ASSETS_FOLDER, SupportFiles.GAMEOBJECT_FOLDER)),
            (SupportFiles.ALL_TEXTURE_BIN, Path.Combine(SupportFiles.ASSETS_FOLDER, SupportFiles.TEXTURE_FOLDER)),
            (SupportFiles.ALL_MATRIX_ID_BIN, SupportFiles.MATRIX_DATA_ID_FOLDER),
            (SupportFiles.ALL_ID_BIN, SupportFiles.ID_FOLDER),
            (SupportFiles.ALL_SCENARIO_EVENTS_BIN,Path.Combine(SupportFiles.SCENARIO_FOLDER,SupportFiles.SCENARIO_EVEMT_FOLDER))
        };

        foreach (var (filePath, targetFolder) in allFiles)
        {
            CopySupportFileToTargetFolder(filePath, buildDir, targetFolder);
        }
    }

    private void CopySupportFileToTargetFolder(string sourceFilePath, string buildRoot, string targetSubFolder)
    {
        if (!File.Exists(sourceFilePath))
        {
            Debug.LogWarning($"[SupportFilesPostprocessor] Source file not found: {sourceFilePath}");
            return;
        }

        string destPath = Path.Combine(buildRoot, SupportFiles.SUPPORT_ROOT_NAME, SupportFiles.SUPPORT_DATA_NAME, targetSubFolder, Path.GetFileName(sourceFilePath));

        // �R�s�[��t�H���_���쐬
        string destDir = Path.GetDirectoryName(destPath);
        if (!Directory.Exists(destDir))
            Directory.CreateDirectory(destDir);

        // �㏑���R�s�[
        File.Copy(sourceFilePath, destPath, true);
        Debug.Log($"[SupportFilesPostprocessor] Copied {sourceFilePath} -> {destPath}");
    }
}
#endif
