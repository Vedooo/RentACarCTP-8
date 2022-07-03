# RentACarCTP-8
<p>Updates and improvements have been made to make the program work more effectively and efficiently.</p>

<h1>Updates and Upgrades</h1>
<ul>
<li><p>ICoreModule is created for Service Dependency use to services (Core > Utilities > IoC). In addition, CoreModule was created for optional use of services provided by Microsoft for visual studio. (Core > DependencyResolvers) </p></li>
<li><p>Add, Import and Remove methods are initialized in ICacheManager (Core > CrossCuttingConcern > Caching) for cache usage for Microsoft Cache Service. It also inherited the Memory Cache Manager (Core > CrossCuttingConcern > Caching > Microsoft) </p></li>
<li><p>New extension added for ICoreModule optional functions (Core > Extensions)</p></li>
<li><p>Authorization classes added in DB like user,admin,moderator. It is now possible to perform operations on the job classes of objects according to authorization.</p></li>
<li><p>New options initialized for Cache, Transaction and Performance aspects (Core > Aspects > Autofac)</p></li>
</ul>
 
 
