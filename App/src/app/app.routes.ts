import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { AppComponent } from './app.component';
import { AboutComponent } from './components/about/about.component';
import { ServicesComponent } from './components/services/services.component';
import { BlogsComponent } from './components/blogs/blogs.component';
import { DocterExpertsComponent } from './components/docter-experts/docter-experts.component';
import { ContactComponent } from './components/contact/contact.component';
import { VisionMissionComponent } from './components/vision-mission/vision-mission.component';
import { BookingComponent } from './components/booking/booking.component';
import { FormAnswerQuestionComponent } from './components/form-answer-question/form-answer-question.component';

export const routes: Routes = [
    { path: '',component: HomeComponent   },
    {path: 'about', component: AboutComponent},
    {path: 'services', component: ServicesComponent},
    {path: 'blogs', component: BlogsComponent},
    {path: 'docter_experts', component: DocterExpertsComponent},
    {path: 'vision-mission', component: VisionMissionComponent},
    {path: 'contact', component: ContactComponent},
    {path: 'booking', component: BookingComponent},
    {path:'faq', component: FormAnswerQuestionComponent}
];
