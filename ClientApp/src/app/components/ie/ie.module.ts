import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { NzModule } from '../../common/nz.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { CommonCompsModule } from '../common-comps/common-comps.module';
import { IeComponent } from './ie.component';
import { IeTemplateComponent } from './ie-template/ie-template.component';

const routes: Routes = [
  {path: "", component: IeTemplateComponent, children: [
    {path: "", component: IeComponent},
  ]}
];

@NgModule({
  declarations: [
    IeComponent,
    IeTemplateComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    CommonCompsModule,
    NzModule,
    ReactiveFormsModule,
    FormsModule
  ]
})
export class IeModule { }
