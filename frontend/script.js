const apiUrl = 'https://localhost:7256/api/task/';

//obter a lista de tarefas
async function getTasks() {
    try {
        const response = await fetch(apiUrl);
        if (!response.ok) {                                     //verifica se a resposta pe ok
            throw new Error(`erro status: ${response.status}`);
        }
        const tasks = await response.json();
        const taskList = document.getElementById('tasks');
        taskList.innerHTML = '';
        tasks.forEach(task => {
            const li = document.createElement('li');
            li.classList.add('list-group-item');
            li.innerHTML = `
                <strong>${task.title}</strong> - ${task.description} - ${new Date(task.dueDate).toLocaleDateString()} - Priority: ${task.priority}
                <button class="btn btn-warning btn-sm float-right ml-2" onclick="editTask(${task.id})">Edit</button>
                <button class="btn btn-danger btn-sm float-right" onclick="deleteTask(${task.id})">Delete</button>
            `;
            taskList.appendChild(li);
        });
    } catch (error) {
        console.error('Erro ao buscar tasks:', error);
        alert('erro ao buscar tasks, log no console...');
    }
}

// Função para adicionar/atualizar tarefas
async function saveTask(task) {
    try {
        // Verifique se o ID é um número ou undefined
        if (task.id && isNaN(task.id)) {
            throw new Error('O ID da tarefa deve ser um número.');
        }

        const method = task.id ? 'PUT' : 'POST';
        const url = task.id ? `${apiUrl}${task.id}` : apiUrl;
        const response = await fetch(url, {
            method: method,
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                title: task.title,
                description: task.description,
                dueDate: task.dueDate,
                priority: task.priority
            })
        });

        if (!response.ok) {
            const errorText = await response.text(); // Obter o texto do erro 
            throw new Error(`Erro oa salvar tarefa. status: ${response.status} - ${errorText}`);
        }

        // Verifica se a resposta tem corpo e processa se necessário
        if (response.status !== 204) { // 204 No Content
            await response.json(); // Espera a resposta ser processada
        }

        getTasks(); // Atualiza a lista de tarefas após salvar
        clearForm();
    } catch (error) {
        console.error('Erro ao salvar task:', error);
        alert(`Falha ao salvar tarefa. Erro: ${error.message}`);
    }
}



// Função que deletaa uma tarefa
async function deleteTask(id) {
    try {
        const response = await fetch(`${apiUrl}${id}`, {
            method: 'DELETE'
        });
        if (!response.ok) {
            throw new Error(`Erro status: ${response.status}`);
        }
        getTasks(); // update na lista de tarefas após excluir
    } catch (error) {
        console.error('Error ao deletar task:', error);
        alert('não foi possível deletar task, olhe o console.');
    }
}

// Função que preenche o formulário com os dados de uma tarefa para editar
function editTask(id) {
    fetch(`${apiUrl}${id}`)
        .then(response => response.json())
        .then(task => 
        {
            //Pega pelo id e atribui aos campos os valores da task que foi feito fetch na api
            document.getElementById('task-id').value = task.id;
            document.getElementById('title').value = task.title;
            document.getElementById('description').value = task.description;
            document.getElementById('due-date').value = task.dueDate.split('T')[0];
            document.getElementById('priority').value = task.priority;
        })
        .catch(error => {
            console.error('Error ao buscar task:', error);
            alert('Falha ao buscar task, verificar console');
        });
}

// Função que limpa o formulário
function clearForm() 
{
    //Pega pelo Id os campos e seta o valor para nada
    document.getElementById('task-id').value = '';
    document.getElementById('title').value = '';
    document.getElementById('description').value = '';
    document.getElementById('due-date').value = '';
    document.getElementById('priority').value = '';
}

// Configura o formulário
document.getElementById('task-form').addEventListener('submit', function(event) {
    event.preventDefault();

    const id = document.getElementById('task-id').value;
    const title = document.getElementById('title').value;
    const description = document.getElementById('description').value;
    const dueDate = document.getElementById('due-date').value;
    const priority = parseInt(document.getElementById('priority').value, 10);

    //Checa se é negativo o campo de prioridade e exibe um alerta
    if (isNaN(priority) || priority < 0) {
        alert('Prioridade não pode ser negativo!');
        return;
    }

    //Cria a task 
    const task = { id, title, description, dueDate, priority };
    //executa o save
    saveTask(task);
});

// Carrega a lista de tarefas com os itens na memória ao iniciar a página
getTasks();
