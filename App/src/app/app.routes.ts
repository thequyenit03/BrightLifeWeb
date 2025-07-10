import { Routes } from '@angular/router';
import { HomeComponent } from './components/client/home/home.component';
import { AppComponent } from './app.component';
import { AboutComponent } from './components/client/about/about.component';
import { ServicesComponent } from './components/client/services/services.component';
import { BlogsComponent } from './components/client/blogs/blogs.component';
import { DocterExpertsComponent } from './components/client/docter-experts/docter-experts.component';
import { ContactComponent } from './components/client/contact/contact.component';
import { VisionMissionComponent } from './components/client/vision-mission/vision-mission.component';
import { BookingComponent } from './components/client/booking/booking.component';
import { FormAnswerQuestionComponent } from './components/client/form-answer-question/form-answer-question.component';
import { LoginComponent } from './components/client/login/login.component';
import { RegisterComponent } from './components/client/register/register.component';

export const routes: Routes = [
    { path: '',component: HomeComponent   },
    {path: 'about', component: AboutComponent},
    {path: 'services', component: ServicesComponent},
    {path: 'blogs', component: BlogsComponent},
    {path: 'docter_experts', component: DocterExpertsComponent},
    {path: 'vision-mission', component: VisionMissionComponent},
    {path: 'contact', component: ContactComponent},
    {path: 'booking', component: BookingComponent},
    {path:'faq', component: FormAnswerQuestionComponent},
    {path: 'login', component: LoginComponent},
    {path: 'register', component: RegisterComponent} 
];
