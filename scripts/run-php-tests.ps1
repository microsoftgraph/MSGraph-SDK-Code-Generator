# https://docs.microsoft.com/en-us/azure/devops/pipelines/ecosystems/php?view=azure-devops

php -v
composer install --no-interaction --prefer-dist
./vendor/bin/phpunit --exclude-group functional