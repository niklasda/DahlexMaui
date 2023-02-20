# DahlexMaui

https://png2svg.com/

Time	Device Name	Type	PID	Tag	Message
02-20 23:03:41.526	Samsung SM-G973F	Info	21315	MonoDroid	android.runtime.JavaProxyThrowable: Microsoft.Maui.Controls.Xaml.XamlParseException: Position 37:67. StaticResource not found for key SmallButtonStyle
   at Microsoft.Maui.Controls.Xaml.ApplyPropertiesVisitor.ProvideValue(Object& value, ElementNode node, Object source, XmlName propertyName)
   at Microsoft.Maui.Controls.Xaml.ApplyPropertiesVisitor.Visit(ElementNode node, INode parentNode)
   at Microsoft.Maui.Controls.Xaml.ElementNode.Accept(IXamlNodeVisitor visitor, INode parentNode)
   at Microsoft.Maui.Controls.Xaml.ElementNode.Accept(IXamlNodeVisitor visitor, INode parentNode)
   at Microsoft.Maui.Controls.Xaml.ElementNode.Accept(IXamlNodeVisitor visitor, INode parentNode)
   at Microsoft.Maui.Controls.Xaml.ElementNode.Accept(IXamlNodeVisitor visitor, INode parentNode)
   at Microsoft.Maui.Controls.Xaml.ElementNode.Accept(IXamlNodeVisitor visitor, INode parentNode)
   at Microsoft.Maui.Controls.Xaml.RootNode.Accept(IXamlNodeVisitor visitor, INode parentNode)
   at Microsoft.Maui.Controls.Xaml.XamlLoader.Visit(RootNode rootnode, HydrationContext visitorContext, Boolean useDesignProperties)
   at Microsoft.Maui.Controls.Xaml.XamlLoader.Load(Object view, String xaml, Assembly rootAssembly, Boolean useDesignProperties)
   at Microsoft.Maui.Controls.Xaml.XamlLoader.Load(Object view, String xaml, Boolean useDesignProperties)
   at Microsoft.Maui.Controls.Xaml.XamlLoader.Load(Object view, Type callingType)
   at Microsoft.Maui.Controls.Xaml.Extensions.LoadFromXaml[BoardPage](BoardPage view, Type callingType)
   at DahlexApp.Views.Board.BoardPage.InitializeComponent()
   at DahlexApp.Views.Board.BoardPage..ctor(BoardViewModel vm)
   at System.Reflection.RuntimeConstructorInfo.Invoke(BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteRuntimeResolver.VisitConstructor(ConstructorCallSite constructorCallSite, RuntimeResolverContext context)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteVisitor`2[[Microsoft.Extensions.DependencyInjection.ServiceLookup.RuntimeResolverContext, Microsoft.Extensions.DependencyInjection, Version=7.0.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60],[System.Object, System.Private.CoreLib, Version=7.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]].VisitCallSiteMain(ServiceCallSite callSite, RuntimeResolverContext argument)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteRuntimeResolver.VisitDisposeCache(ServiceCallSite transientCallSite, RuntimeResolverContext context)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteVisitor`2[[Microsoft.Extensions.DependencyInjection.ServiceLookup.RuntimeResolverContext, Microsoft.Extensions.DependencyInjection, Version=7.0.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60],[System.Object, System.Private.CoreLib, Version=7.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]].VisitCallSite(ServiceCallSite callSite, RuntimeResolverContext argument)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteRuntimeResolver.Resolve(ServiceCallSite callSite, ServiceProviderEngineScope scope)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.RuntimeServiceProviderEngine.<>c__DisplayClass4_0.<RealizeService>b__0(ServiceProviderEngineScope scope)
   at Microsoft.Extensions.DependencyInjection.ServiceProvider.GetService(Type serviceType, ServiceProviderEngineScope serviceProviderEngineScope)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.ServiceProviderEngineScope.GetService(Type serviceType)
   at Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetService[BoardPage](IServiceProvider provider)
   at DahlexApp.Logic.Services.NavigationService.ResolvePage[BoardPage]()
   at DahlexApp.Logic.Services.NavigationService.<NavigateToBoardPage>d__5`1[[DahlexApp.Views.Board.BoardPage, DahlexApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]].MoveNext()
   at DahlexApp.Views.Start.StartViewModel.<>c__DisplayClass0_0.<<-ctor>b__1>d.MoveNext()
