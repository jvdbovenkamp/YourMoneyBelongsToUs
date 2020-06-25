# Is this still needed with latest versions?

Set-ExecutionPolicy Bypass -Scope Process -Force;
iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))


# https://learnk8s.io/blog/installing-docker-and-kubernetes-on-windows
# choco install docker-desktop -y ## Reboot
choco install minikube -y
minikube start --vm-driver=hyperv

