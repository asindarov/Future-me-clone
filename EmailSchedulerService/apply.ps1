param(
    [Parameter(Mandatory)] [string]$author,
    [Parameter(Mandatory)] [string]$version,
    [string]$logLevel,
    [bool] [Alias("run-liquibase")] $run_liquibase = $False,
    [Alias("migration-version-1")][string] $ef_core_migration_version1,
    [Alias("migration-version-2")][string] $ef_core_migration_version2
)


if ($null -eq $author -or $null -eq $version)
{
    Write-Host "Please, specify author name and version for changelog generation! FYI: Use -author YOURNAME_HERE -version MIGRATION_VERSION" -f Red
    exit
}

$changelog_path = "./changelog__version-${version}.sql"

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
    
    $sql_scripts = Get-Content $changelog_path
    
    <# TODO: 
            Consider the case where the changelog is duplicated 
            by checking $sql_scripts in previous changelog file
            if chagelog is duplicated then exit the progam!!!
    #>
    
    Write-Host "Writing sql scripts to changelog file"
    Write-Host "Author: ${author}, version : ${version}"

    $actual_changelog_content = "--liquibase formatted sql`n`n--changeset ${author}:${version}`n${sql_scripts}"

    Write-Host $actual_changelog_content

    Set-Content -Path $changelog_path -Value $actual_changelog_content
    
    if (![string]::IsNullOrEmpty($logLevel) -and $run_liquibase)
    {
        liquibase --changeLogFile=$changelog_path update --log-level $logLevel
    }
    elseif ($run_liquibase)
    {
        liquibase --changeLogFile=$changelog_path update
    }

}
catch
{
    Remove-Item $changelog_path
    "An error occurred that could not be resolved."   

}