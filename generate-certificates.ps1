# Source: https://stackoverflow.com/a/62060315
# Generate self-signed certificate to be used by IdentityServer.
# When using localhost - API cannot see the IdentityServer from within the docker-compose'd network.
# You have to run this script as Administrator (open Powershell by right click -> Run as Administrator).

$ErrorActionPreference = "Stop"

$rootCN = "DagAirIdentityServerDockerRootCert"
$identityServerCNs = "identity_server", "localhost", "host.docker.internal"
$webApiCNs = "web_admin_app", "localhost", "host.docker.internal"

$alreadyExistingCertsRoot = Get-ChildItem -Path Cert:\LocalMachine\My -Recurse | Where-Object {$_.Subject -eq "CN=$rootCN"}
$alreadyExistingCertsIdentityServer = Get-ChildItem -Path Cert:\LocalMachine\My -Recurse | Where-Object {$_.Subject -eq ("CN={0}" -f $identityServerCNs[0])}
$alreadyExistingCertsApi = Get-ChildItem -Path Cert:\LocalMachine\My -Recurse | Where-Object {$_.Subject -eq ("CN={0}" -f $webApiCNs[0])}

if ($alreadyExistingCertsRoot.Count -eq 1) {
    Write-Output "Skipping creating Root CA certificate as it already exists."
    $testRootCA = [Microsoft.CertificateServices.Commands.Certificate] $alreadyExistingCertsRoot[0]
} else {
    $testRootCA = New-SelfSignedCertificate -Subject $rootCN -KeyUsageProperty Sign -KeyUsage CertSign -CertStoreLocation Cert:\LocalMachine\My
}

if ($alreadyExistingCertsIdentityServer.Count -eq 1) {
    Write-Output "Skipping creating Identity Server certificate as it already exists."
    $identityServerCert = [Microsoft.CertificateServices.Commands.Certificate] $alreadyExistingCertsIdentityServer[0]
} else {
    # Create a SAN cert for both identity-server and localhost.
    $identityServerCert = New-SelfSignedCertificate -DnsName $identityServerCNs -Signer $testRootCA -CertStoreLocation Cert:\LocalMachine\My
}

if ($alreadyExistingCertsApi.Count -eq 1) {
    Write-Output "Skipping creating Web Admin App certificate as it already exists."
    $webAdminApp = [Microsoft.CertificateServices.Commands.Certificate] $alreadyExistingCertsApi[0]
} else {
    # Create a SAN cert for both web-api and localhost.
    $webAdminApp = New-SelfSignedCertificate -DnsName $webApiCNs -Signer $testRootCA -CertStoreLocation Cert:\LocalMachine\My
}

# Export it for docker container to pick up later.
$password = ConvertTo-SecureString -String "type-your-password" -Force -AsPlainText

$rootCertPathPfx = "certs"
$identityServerCertPath = "src/DagAir.IdentityServer/certs"
$webAdminAppPath = "src/DagAir_WebApplications/DagAir.WebAdminApp/certs"

New-Item -ItemType Directory -Force -Path $rootCertPathPfx
New-Item -ItemType Directory -Force -Path $identityServerCertPath
New-Item -ItemType Directory -Force -Path $webAdminAppPath

Export-PfxCertificate -Cert $testRootCA -FilePath "$rootCertPathPfx/dagair-root-cert.pfx" -Password $password | Out-Null
Export-PfxCertificate -Cert $identityServerCert -FilePath "$identityServerCertPath/DagAir.IdentityServer.pfx" -Password $password | Out-Null
Export-PfxCertificate -Cert $webAdminApp -FilePath "$webAdminAppPath/DagAir.WebAdminApp.pfx" -Password $password | Out-Null

# Export .cer to be converted to .crt to be trusted within the Docker container.
$rootCertPathCer = "certs/dagair-root-cert.cer"
Export-Certificate -Cert $testRootCA -FilePath $rootCertPathCer -Type CERT | Out-Null

# Trust it on your host machine.
$store = New-Object System.Security.Cryptography.X509Certificates.X509Store "Root","LocalMachine"
$store.Open("ReadWrite")

$rootCertAlreadyTrusted = ($store.Certificates | Where-Object {$_.Subject -eq "CN=$rootCN"} | Measure-Object).Count -eq 1

if ($rootCertAlreadyTrusted -eq $false) {
    Write-Output "Adding the root CA certificate to the trust store."
    $store.Add($testRootCA)
}

$store.Close()