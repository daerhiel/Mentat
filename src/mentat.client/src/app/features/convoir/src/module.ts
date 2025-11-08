import { NgModule } from '@angular/core';

import { Convoir } from './convoir';

const declarations = [
  Convoir
];

@NgModule({
  imports: [...declarations],
  exports: [...declarations]
})
export class ConvoirModule { }
