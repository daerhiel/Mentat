import { Component, inject, signal } from '@angular/core';
import { rxResource } from '@angular/core/rxjs-interop';
import { RouterOutlet } from '@angular/router';

import { ExplorerService } from './features/explorer';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  readonly #explorer = inject(ExplorerService);

  title = 'mentat.client';

  protected readonly _word = signal("Újlipótváros");

  protected readonly _lexeme = rxResource({
    params: this._word,
    stream: ({ params }) => this.#explorer.lexicon.getLexeme(params)
  });
}
