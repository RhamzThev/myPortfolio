import tkinter as tk
from tkinter import ttk

from view import View
from controller import Controller

class App(tk.Tk):

    def __init__(self):
        super().__init__()
        
        # INIT WINDOW
        self.__init_window()

        
        
    # GETTERS AND SETTERS

    def set_controller(self, controller):
        self.controller = controller

    # INITIALIZATIONS
        
    def __init_window(self):
        self.title('Window')
        self.geometry('500x500')

    def __init_widgets(self):
        ttk.Button(self.frame, text="Win", command=self.__win).pack()
        ttk.Button(self.frame, text="Lose", command=self.__lose).pack()

    # CONTROLLER FUNCTIONS

    def __win(self):
        if self.controller:
            self.controller.win()

    def __lose(self):
        if self.controller:
            self.controller.lose()



    



def main():
    app = App()
    app.mainloop()

if __name__ == "__main__": main()