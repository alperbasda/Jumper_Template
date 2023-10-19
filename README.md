# Jumper_Template

<b>Jumper_Template projesi ne işe yarar ?</b>
<br/>
Jumper_Template projesi CQRS pattern için proje templatelerini oluşturmaya yarayan bir template enginedir.
<br/>
<b>Template projesi nasıl ayağa kaldırılır ?</b>
<br/>
1-) Proje dosyalarını indirin.
<br/>
2-) CqrsTemplatePack.Creator.csproj dosyasını notepad vb bir uygulama ile açarak <PackageOutputPath> düğümünü bat dosyalarının bulundugu dizine ayarlayın.
<br/>
3-) Console veya powershell açarak aşagıdaki komutları sırası ile çalıştırın
<br/>
    a-) dotnet new install Microsoft.TemplateEngine.Authoring.Templates (Template engine yazabilmek için gerekli olan pakedi kurar.) veya NewTemplatePackBuilder.bat
    <br/>
    b-) dotnet pack komutunu çalıştırın.Bu adım sonrasında ilgili dosyada nuget pakedinizi görmeniz gerekir. (İlgili templateleri nuget pakedi haline getirir.) veya CreateTemplateNuget.bat
    <br/>
    c-)  dotnet new install "CqrsTemplatePack.Creator.1.0.0.nupkg" proje templatelerinizin nuget pakedinden IDEnize aktarılmasını sağlar. veya InstallTemplates.bat
    <br/>
4-) dotnet new projetipi -o cıktıklasörü --name projeAdı --solutionName ÇÖzümAdı --force komutu ile isteğiniz templateten proje olusturabilirsiniz.
<br/>

Not : Proje tiplerini template.config dosyalarında shortName alanında bulabilirsinz.
