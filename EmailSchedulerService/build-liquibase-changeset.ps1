param(
    [Parameter(Mandatory)] [string]$author,
    [Parameter(Mandatory)] [string]$version,
    [string]$logLevel,
    [bool] [Alias("run-liquibase")] $run_liquibase = $False,
    [Alias("migration-version-1")][string] $ef_core_migration_version1,
    [Alias("migration-version-2")][string] $ef_core_migration_version2
)

if ($version -ne 1 -and ([string]::IsNullOrEmpty($ef_core_migration_version1) -or [string]::IsNullOrEmpty($ef_core_migration_version2)))
{
    Write-Host "Please, specify migration versions, because it is not your first version of changesets. FYI: Use initialize these flags -migration-version-1 PREVIOUS_MIGRATION_NAME -migration-version-2 NEWLY_CREATED_MIGRATION_NAME" -f Red
    exit
}

if ($null -eq $author -or $null -eq $version)
{
    Write-Host "Please, specify author name and version for changelog generation! FYI: Use -author YOURNAME_HERE -version MIGRATION_VERSION" -f Red
    exit
}

$changelog_path = "db-liquibase/changelog/changesets/changelog__version-${version}.sql"

if (Test-Path -Path $changelog_path)
{
    Write-Host "the specified version's log file is already created!" -f Red
    exit
}

try{
    
    if (![string]::IsNullOrEmpty($ef_core_migration_version1) -and ![string]::IsNullOrEmpty($ef_core_migration_version2))
    {
        dotnet ef migrations script $ef_core_migration_version1 $ef_core_migration_version2 -o $changelog_path
    }
    else
    {
        dotnet ef migrations script -o $changelog_path
    }
    
    $sql_scripts = $message = (Get-Content  $changelog_path) -join "`n"
    
    <# TODO: 
            Consider the case where the changelog is duplicated 
            by checking $sql_scripts in previous changelog file
            if chagelog is duplicated then exit the progam!!!
    #>
    
    Write-Host "Writing sql scripts to changelog file"

    $actual_changelog_content = "--liquibase formatted sql`n`n--changeset ${author}:${version}`n${sql_scripts}"

    Write-Host $actual_changelog_content

    Set-Content -Path $changelog_path -Value $actual_changelog_content
    
#    $actual_changelog_content | Out-File -FilePath $changelog_path
    
    if (![string]::IsNullOrEmpty($logLevel) -and $run_liquibase)
    {
        Write-Host "Running liquibase with log level"

        liquibase update --log-level $logLevel
    }
    elseif ($run_liquibase)
    {
        Write-Host "Running liquibase"
        
        liquibase update
    }

}
catch
{
    Remove-Item $changelog_path
    "An error occurred that could not be resolved."
}