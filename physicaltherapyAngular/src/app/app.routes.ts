import { Routes } from '@angular/router';
import { ExercisesComponent } from './components/exercises/exercises.component';
import { CategoryComponent } from './components/category/category.component';

export const routes: Routes = [
    { path: "", pathMatch: "full", redirectTo: "categories" },
    { path: "categories", component: CategoryComponent },
    { path: "exercises", component: ExercisesComponent },
];
