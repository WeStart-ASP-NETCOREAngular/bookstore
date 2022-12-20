import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './Components/Home/home.component';
import { CategoryComponent } from './Components/admin/category/category.component';
import { DashboardComponent } from './Components/admin/dashboard/dashboard.component';
import { ReactiveFormsModule } from '@angular/forms';
import { PublisherComponent } from './Components/admin/publisher/publisher.component';
import { PublishernewComponent } from './Components/admin/publishernew/publishernew.component';
import { LoginComponent } from './Components/authentication/login/login.component';
import { RegisterComponent } from './Components/authentication/register/register.component';
import { JwtInterceptor } from './services/jwt.interceptor';
import { ForbiddenComponent } from './Components/forbidden/forbidden.component';
import { NgxPaginationModule } from 'ngx-pagination';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    CategoryComponent,
    DashboardComponent,
    PublisherComponent,
    PublishernewComponent,
    LoginComponent,
    RegisterComponent,
    ForbiddenComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    NgxPaginationModule

  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
