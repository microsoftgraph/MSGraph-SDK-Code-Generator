param([String]$metadata='https://graph.microsoft.com/v1.0/$metadata')

$ErrorActionPreference = "Stop"

# pull docs 
git clone --depth 1 https://github.com/microsoftgraph/microsoft-graph-docs.git docs

# Get metadata
Invoke-WebRequest $metadata -outfile metadata.xml

# apidoctor the $metadata
apidoc publish-edmx --path 'docs\api-reference\v1.0' --source metadata.xml --output finalmetadata --skip-generation

# Generate new Types from metadata
typewriter -v Info -l TypeScript -m 'finalmetadata/metadata.xml' -o generated

# Based on Language pull repo 
git clone --depth 1 https://github.com/microsoftgraph/msgraph-sdk-dotnet.git source

# Create branch
set-location source
git checkout -b metadata-2018-09-14

# delete generated files
remove-item -Recurse .\source\src\Microsoft.Graph\Models\Generated\
remove-item -Recurse .\source\src\Microsoft.Graph\Requests\Generated\

# copy generated files
move-item .\generated\com\microsoft\graph\model .\source\src\Microsoft.Graph\Models\Generated\
move-item .\generated\com\microsoft\graph\requests .\source\src\Microsoft.Graph\Requests\Generated\

# commit and push branch
