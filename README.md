# Jumper_Template

Jumper_Template projesi ne işe yarar ?
Jumper_Template projesi CQRS pattern için proje templatelerini oluşturmaya yarayan bir template enginedir.

Template projesi nasıl ayağa kaldırılır ?

1-) Proje dosyalarını indirin.
2-) CqrsTemplatePack.Creator.csproj dosyasını notepad vb bir uygulama ile açarak <PackageOutputPath> düğümünü bat dosyalarının bulundugu dizine ayarlayın.
3-) Console veya powershell açarak aşagıdaki komutları sırası ile çalıştırın
    a-) dotnet new install Microsoft.TemplateEngine.Authoring.Templates (Template engine yazabilmek için gerekli olan pakedi kurar.) veya NewTemplatePackBuilder.bat
    b-) dotnet pack komutunu çalıştırın.Bu adım sonrasında ilgili dosyada nuget pakedinizi görmeniz gerekir. (İlgili templateleri nuget pakedi haline getirir.) veya CreateTemplateNuget.bat
    c-)  dotnet new install "CqrsTemplatePack.Creator.1.0.0.nupkg" proje templatelerinizin nuget pakedinden IDEnize aktarılmasını sağlar. veya InstallTemplates.bat
4-) dotnet new projetipi -o cıktıklasörü --name projeAdı --solutionName ÇÖzümAdı --force komutu ile isteğiniz templateten proje olusturabilirsiniz.

Not : Proje tiplerini template.config dosyalarında shortName alanında bulabilirsinz.
